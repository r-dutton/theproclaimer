[web] GET /api/hmrc/feedback  (Workpapers.Next.API.Controllers.Workpapers.HmrcController.GetFraudHeaderFeedback)  [L89–L96] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request GetHmrcFraudHeaderFeedbackQuery [L93]
    └─ handled_by DataGet.Integrations.Hmrc.Api.Queries.GetHmrcFraudHeaderFeedbackQueryHandler.Handle [L14–L27]
      └─ uses_client HmrcClient [L25]
      └─ uses_service HmrcClient
        └─ method GetFraudHeaderFeedback [L25]
          └─ implementation DataGet.Integrations.Hmrc.Api.Services.HmrcClient.GetFraudHeaderFeedback [L35-L298]

