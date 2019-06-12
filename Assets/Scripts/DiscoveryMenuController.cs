using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DiscoveryMenuController : SlideInMenu
{
    public DiscoveryEntity discoveryEntityPrefab;

    public GridLayoutGroup discoveryContainer;

    public Text selectedSecretText;
    public Text assignedDiscoveryText;

    public Image discoveryBG;

    public BookController bookController;

    private SecretData _selectedSecret;

    private bool _overDiscoveryInput;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        DiscoveryManager _discoveryManager = Director.GetManager<DiscoveryManager>();
        while (_discoveryManager == null)
        {
            yield return new WaitForEndOfFrame();
            _discoveryManager = Director.GetManager<DiscoveryManager>();
        }
        selectedSecretText.enabled = false;
        assignedDiscoveryText.text = "No secret selected";
        _discoveryManager.OnNewDiscovery += NewDiscovery;
        bookController.OnSecretClicked += SecretClicked;
    }

    void NewDiscovery(DiscoveryData data)
    {
        DiscoveryEntity discovery = Instantiate(discoveryEntityPrefab, discoveryContainer.transform);
        discovery.discoveryData = data;
        discovery.OnPickup += RemoveDiscovery;
        discovery.OnDrop += DropDiscovery;
    }

    void RemoveDiscovery(DraggableUI discovery)
    {
        discovery.transform.SetParent(transform);
        discoveryBG.color = Color.green / 2;
    }

    void DropDiscovery(DraggableUI discovery)
    {
        discoveryBG.color = Color.white / 2;
        discovery.transform.SetParent(discoveryContainer.transform);
        if (_selectedSecret != null && _overDiscoveryInput)
        {
            Director.GetManager<SecretsManager>().AssignDiscoveryToData(_selectedSecret, ((DiscoveryEntity)discovery).discoveryData);
            assignedDiscoveryText.text = ((DiscoveryEntity)discovery).discoveryData.name;
        }
    }

    public void SetOverDiscoveryInput(bool overDiscoveryInput)
    {
        _overDiscoveryInput = overDiscoveryInput;
    }

    void SecretClicked(SecretData secret)
    {
        _selectedSecret = secret;
        selectedSecretText.enabled = true;
        selectedSecretText.text = secret.name;
        DiscoveryData assignedDiscovery = Director.GetManager<SecretsManager>().GetAssignedDiscovery(secret);
        if (assignedDiscovery == null)
        {
            assignedDiscoveryText.text = "No assigned discovery";
        }
        else
        {
            assignedDiscoveryText.text = assignedDiscovery.name;
        }

    }

}
