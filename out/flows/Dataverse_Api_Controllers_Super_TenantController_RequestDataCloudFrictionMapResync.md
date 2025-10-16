[web] POST /api/super/tenants/friction-map-resync  (Dataverse.Api.Controllers.Super.TenantController.RequestDataCloudFrictionMapResync)  [L210–L216] status=201 [auth=Authentication.MasterPolicy]
  └─ uses_service IDataCloudService (AddScoped)
    └─ method ActionFrictionMapSyncAsync [L213]
      └─ implementation Dataverse.Utilities.ApiCaller.Services.DataCloudService.ActionFrictionMapSyncAsync [L7-L35]

