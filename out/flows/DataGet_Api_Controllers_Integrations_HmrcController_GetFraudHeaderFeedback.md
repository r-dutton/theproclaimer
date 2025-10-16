[web] GET /api/hmrc/feedback  (DataGet.Api.Controllers.Integrations.HmrcController.GetFraudHeaderFeedback)  [L79–L89] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ uses_service HmrcApiService (AddScoped)
    └─ method GetFraudHeaderFeedback [L84]
      └─ implementation DataGet.Integrations.Hmrc.Api.Services.HmrcApiService.GetFraudHeaderFeedback [L17-L78]

