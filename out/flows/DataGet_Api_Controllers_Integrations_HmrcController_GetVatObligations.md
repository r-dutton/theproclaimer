[web] POST /api/hmrc/vat/obligations  (DataGet.Api.Controllers.Integrations.HmrcController.GetVatObligations)  [L42–L53] status=201 [auth=Authentication.MachineToMachinePolicy]
  └─ uses_service HmrcApiService (AddScoped)
    └─ method GetVatObligations [L48]
      └─ implementation DataGet.Integrations.Hmrc.Api.Services.HmrcApiService.GetVatObligations [L17-L78]

