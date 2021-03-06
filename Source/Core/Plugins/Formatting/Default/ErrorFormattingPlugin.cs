﻿using System;
using System.Net.Mail;
using CodeSmith.Core.Component;
using Exceptionless.Core.Extensions;
using Exceptionless.Core.Mail.Models;
using Exceptionless.Core.Queues.Models;
using Exceptionless.Extensions;
using Exceptionless.Models;
using RazorSharpEmail;
using MailMessage = System.Net.Mail.MailMessage;

namespace Exceptionless.Core.Plugins.Formatting {
    [Priority(10)]
    public class ErrorFormattingPlugin : IFormattingPlugin {
        private readonly IEmailGenerator _emailGenerator;

        public ErrorFormattingPlugin(IEmailGenerator emailGenerator) {
            _emailGenerator = emailGenerator;
        }

        private bool ShouldHandle(PersistentEvent ev) {
            return ev.IsError();
        }

        public string GetStackSummaryHtml(PersistentEvent ev) {
            if (!ShouldHandle(ev))
                return null;

            // TODO: Actually implement GetStackSummaryHtml.
            return ev.Message ?? "None";
        }

        public string GetEventSummaryHtml(PersistentEvent ev) {
            if (!ShouldHandle(ev))
                return null;

            // TODO: Actually implement GetEventSummaryHtml.
            return ev.Message ?? "None";
        }

        public string GetStackTitle(PersistentEvent ev) {
            if (!ShouldHandle(ev))
                return null;

            // TODO: Actually implement GetStackTitle.
            return ev.Message ?? "None";
        }

        public MailMessage GetEventNotificationMailMessage(EventNotification model) {
            if (!ShouldHandle(model.Event))
                return null;

            var error = model.Event.GetError();
            var stackingTarget = error.GetStackingTarget();
            var requestInfo = model.Event.GetRequestInfo();

            string notificationType = String.Concat(error.Type, " Occurrence");
            if (model.IsNew)
                notificationType = String.Concat("New ", error.Type);
            else if (model.IsRegression)
                notificationType = String.Concat(error.Type, " Regression");

            if (model.IsCritical)
                notificationType = String.Concat("Critical ", notificationType);

            var mailerModel = new EventNotificationModel(model) {
                BaseUrl = Settings.Current.BaseURL,
                NotificationType = notificationType,
                Url = requestInfo != null ? requestInfo.GetFullPath(true, true, true) : null,
                Error = stackingTarget.Error,
                Method = stackingTarget.Method,
            };

            return _emailGenerator.GenerateMessage(mailerModel, "Notice");
        }

        public string GetEventViewName(PersistentEvent ev) {
            if (!ShouldHandle(ev))
                return null;

            // TODO: Actually implement GetEventViewName.
            return String.Empty;
        }
    }
}
