[web] POST /api/hmrc/validate  (Workpapers.Next.API.Controllers.Workpapers.HmrcController.ValidateFraudHeaders)  [L80–L87] status=201 [auth=AuthorizationPolicies.User]
  └─ sends_request ValidateHmrcFraudHeadersQuery [L84]
    └─ handled_by DataGet.Integrations.Hmrc.Api.Queries.ValidateHmrcFraudHeadersQueryHandler.Handle [L22–L35]
      └─ uses_client HmrcClient [L33]
      └─ uses_service HmrcClient
        └─ method ValidateFraudHeaders [L33]
          └─ implementation DataGet.Integrations.Hmrc.Api.Services.HmrcClient.ValidateFraudHeaders [L35-L298]
      └─ validation _hmrcClient.ValidateFraudHeaders [L33]

