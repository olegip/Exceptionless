using System;
using Exceptionless.Models;
using FluentValidation;

namespace Exceptionless.Core.Validation {
    public class PersistentEventValidator : AbstractValidator<PersistentEvent> {
        public PersistentEventValidator() {
            RuleFor(e => e.OrganizationId).NotEmpty().WithMessage("Please specify a valid organization id.");
            RuleFor(e => e.ProjectId).NotEmpty().WithMessage("Please specify a valid project id.");
            RuleFor(e => e.StackId).NotEmpty().WithMessage("Please specify a valid stack id.");

            RuleFor(e => e.Type)
                .NotEmpty()
                .Must(BeAValidEventType)
                .WithMessage("Please specify a valid event type.");
            
            RuleFor(e => e.ReferenceId)
                .NotEmpty()
                .Length(8, 32)
                .Unless(u => String.IsNullOrEmpty(u.ReferenceId))
                .WithMessage("ReferenceId must contain between 8 and 32 characters");
        }

        private bool BeAValidEventType(string type) {
            switch (type.ToLower()) {
                case Event.KnownTypes.Error:
                case Event.KnownTypes.NotFound:
                case Event.KnownTypes.Log:
                case Event.KnownTypes.FeatureUsage:
                case Event.KnownTypes.SessionStart:
                case Event.KnownTypes.SessionEnd:
                    break;
                default:
                    return false;
            }

            return true;
        }
    }
}