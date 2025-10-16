[web] POST /api/cloud-capcha/activities  (DataGet.Api.Controllers.Integrations.CloudCapchaController.PostActivities)  [L110–L116] status=201 [auth=Authentication.MachineToMachinePolicy]
  └─ uses_service ICloudCapchaService (AddScoped)
    └─ method CreateActivitiesAsync [L115]
      └─ implementation DataGet.Integrations.CloudCapcha.Api.Services.CloudCapchaService.CreateActivitiesAsync [L15-L216]
        └─ uses_service RequestProcessor
          └─ method CreateActivitiesAsync [L79]
            └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.CreateActivitiesAsync
            └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.CreateActivitiesAsync
            └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.CreateActivitiesAsync
            └─ +1 additional_requests elided
        └─ uses_service TimeSpan
          └─ method CreateActivitiesAsync [L66]
            └─ ... (no implementation details available)
        └─ uses_cache IDistributedCache.SetRecordAsync [write] [L212]
        └─ uses_cache IDistributedCache.GetRecordAsync [read] [L208]
        └─ logs ILogger<CloudCapchaService> [Warning] [L170]
        └─ logs ILogger<CloudCapchaService> [Information] [L127]
        └─ logs ILogger<CloudCapchaService> [Error] [L84]
  └─ impact_summary
    └─ services 1
      └─ ICloudCapchaService
    └─ caches 1
      └─ IDistributedCache

