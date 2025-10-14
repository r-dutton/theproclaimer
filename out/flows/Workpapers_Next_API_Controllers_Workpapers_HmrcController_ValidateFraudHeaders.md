[web] POST /api/hmrc/validate  (Workpapers.Next.API.Controllers.Workpapers.HmrcController.ValidateFraudHeaders)  [L80–L87] [auth=AuthorizationPolicies.User]
  └─ sends_request ValidateHmrcFraudHeadersQuery [L84]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.Hmrc.Api.Queries.ValidateHmrcFraudHeadersQueryHandler.Handle [L22–L35]
      └─ uses_client HmrcClient [L33]
      └─ uses_service HmrcClient
        └─ method ValidateFraudHeaders [L33]

