using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningInteraction : Interaction
{
    [System.Serializable]
    public class ElementTransformation
    {
        public ElementData input;
        public ElementData output;
    }
    public List<ElementTransformation> recipes = new List<ElementTransformation>();


    public override void Interact(ElementInstance element)
    {
        if (element != null)
        {
            foreach (var transformation in recipes)
            {
                if (transformation.input == element.Data)
                {
                    element.Data = transformation.output;
                    Director.GetManager<AchievementManager>().AddCount(element.Data.creationAchievement);
                    break;
                }
            }
        }
    }
}
