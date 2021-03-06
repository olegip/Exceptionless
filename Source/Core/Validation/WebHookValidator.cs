using System;
using Exceptionless.Models.Admin;
using FluentValidation;

namespace Exceptionless.Core.Validation {
    public class WebHookValidator : AbstractValidator<WebHook> {
        public WebHookValidator() {
            RuleFor(w => w.OrganizationId).NotEmpty().WithMessage("Please specify a valid organization id.");
            RuleFor(w => w.ProjectId).NotEmpty().WithMessage("Please specify a valid project id.");
            RuleFor(w => w.Url).NotEmpty().WithMessage("Please specify a valid url.");
            RuleFor(w => w.EventTypes).NotEmpty().WithMessage("Please specify one or more event types.");
        }
    }
}