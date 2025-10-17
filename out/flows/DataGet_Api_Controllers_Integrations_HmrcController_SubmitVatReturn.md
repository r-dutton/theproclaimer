[web] POST /api/hmrc/vat/submit  (DataGet.Api.Controllers.Integrations.HmrcController.SubmitVatReturn)  [L55–L65] status=201 [auth=Authentication.MachineToMachinePolicy]
  └─ uses_service HmrcApiService (AddScoped)
    └─ method SubmitVatReturn [L60]
      └─ implementation DataGet.Integrations.Hmrc.Api.Services.HmrcApiService.SubmitVatReturn [L17-L78]
  └─ impact_summary
    └─ services 1
      └─ HmrcApiService

