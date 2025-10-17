[web] POST /api/hmrc/validate  (DataGet.Api.Controllers.Integrations.HmrcController.ValidateFraudHeaders)  [L67–L77] status=201 [auth=Authentication.MachineToMachinePolicy]
  └─ uses_service HmrcApiService (AddScoped)
    └─ method ValidateFraudHeaders [L72]
      └─ implementation DataGet.Integrations.Hmrc.Api.Services.HmrcApiService.ValidateFraudHeaders [L17-L78]
  └─ impact_summary
    └─ services 1
      └─ HmrcApiService

