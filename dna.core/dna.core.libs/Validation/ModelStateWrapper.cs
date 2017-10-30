using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace dna.core.libs.Validation
{
    /// <summary>
    /// This class used to wrap ModelState from controller
    /// This class created because to avoid depedency of ASP.NET MVC
    /// from Controller to Services
    /// ref: https://www.asp.net/mvc/overview/older-versions-1/models-data/validating-with-a-service-layer-cs
    /// </summary>
    public class ModelStateWrapper : IValidationDictionary
    {
        private ModelStateDictionary _modelState;

        public ModelStateWrapper(ModelStateDictionary modelState)
        {
            _modelState = modelState;
        }

        public bool IsValid
        {
            get { return _modelState.IsValid; }
        }

        public void AddError(string key, string errorMessage)
        {
            _modelState.AddModelError(key, errorMessage);
        }
    }
}
