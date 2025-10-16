[web] POST /api/hmrc/vat/package  (DataGet.Api.Controllers.Integrations.HmrcController.GetVatPackage)  [L29–L40] status=201 [auth=Authentication.MachineToMachinePolicy]
  └─ uses_service HmrcApiService (AddScoped)
    └─ method GetVatPackage [L35]
      └─ implementation DataGet.Integrations.Hmrc.Api.Services.HmrcApiService.GetVatPackage [L17-L78]

