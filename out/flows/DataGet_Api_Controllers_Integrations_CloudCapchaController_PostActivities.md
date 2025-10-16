[web] POST /api/cloud-capcha/activities  (DataGet.Api.Controllers.Integrations.CloudCapchaController.PostActivities)  [L110–L116] status=201 [auth=Authentication.MachineToMachinePolicy]
  └─ uses_service ICloudCapchaService (AddScoped)
    └─ method CreateActivitiesAsync [L115]
      └─ implementation DataGet.Integrations.CloudCapcha.Api.Services.CloudCapchaService.CreateActivitiesAsync [L15-L216]

