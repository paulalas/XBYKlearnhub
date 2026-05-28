using System;
using System.Linq;
using System.Threading.Tasks;
using CMS.DataEngine;
using CMS.FormEngine;
using CMS.OnlineForms;
using LearnHub.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearnHub.Controllers
{
    /// <summary>
    /// Controller for handling form submissions
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FormsController : ControllerBase
    {
        private readonly IInfoProvider<BizFormInfo> bizFormInfoProvider;

        public FormsController(IInfoProvider<BizFormInfo> bizFormInfoProvider)
        {
            this.bizFormInfoProvider = bizFormInfoProvider;
        }

        /// <summary>
        /// Handles Get Started form submission
        /// </summary>
        /// <param name="model">Form data from the client</param>
        /// <returns>JSON response indicating success or failure</returns>
        [HttpPost("get-started")]
        public async Task<IActionResult> SubmitGetStartedForm([FromBody] GetStartedFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Please fill in all required fields correctly.",
                    errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                });
            }

            try
            {
                // Get the form object (you'll need to create this form in Kentico admin first)
                // The form code name should be "GetStarted"
                BizFormInfo formObject = bizFormInfoProvider.Get("GetStarted");

                if (formObject == null)
                {
                    return StatusCode(500, new
                    {
                        success = false,
                        message = "Form configuration not found. Please contact the administrator."
                    });
                }

                // Get the class name of the form
                DataClassInfo formClass = DataClassInfoProvider.GetDataClassInfo(formObject.FormClassID);
                string formClassName = formClass.ClassName;

                // Create a new data record for the form
                BizFormItem newFormItem = BizFormItem.New(formClassName);

                // Set the values for the form's fields
                // Field names must match the fields you create in the Kentico form
                newFormItem.SetValue("FullName", model.Name);
                newFormItem.SetValue("Email", model.Email);
                newFormItem.SetValue("SelectedCourse", model.Course);
                newFormItem.SetValue("UserMessage", model.Message ?? string.Empty);
                newFormItem.SetValue("AgreedToTerms", model.AgreeToTerms);
                newFormItem.SetValue("SubmittedOn", DateTime.Now);

                // Save the form record to the database
                newFormItem.Insert();

                return Ok(new
                {
                    success = true,
                    message = "Thank you for your interest! We'll get back to you soon."
                });
            }
            catch (Exception ex)
            {
                // Log the exception (inject ILogger<FormsController> for proper logging)
                return StatusCode(500, new
                {
                    success = false,
                    message = "An error occurred while submitting the form. Please try again later.",
                    error = ex.Message
                });
            }
        }
    }
}
