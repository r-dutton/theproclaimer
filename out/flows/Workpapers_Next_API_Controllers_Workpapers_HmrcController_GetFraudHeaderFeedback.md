[web] GET /api/hmrc/feedback  (Workpapers.Next.API.Controllers.Workpapers.HmrcController.GetFraudHeaderFeedback)  [L89–L96] [auth=AuthorizationPolicies.User]
  └─ sends_request GetHmrcFraudHeaderFeedbackQuery [L93]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.Hmrc.Api.Queries.GetHmrcFraudHeaderFeedbackQueryHandler.Handle [L14–L27]
      └─ uses_client HmrcClient [L25]
      └─ uses_service HmrcClient
        └─ method GetFraudHeaderFeedback [L25]

