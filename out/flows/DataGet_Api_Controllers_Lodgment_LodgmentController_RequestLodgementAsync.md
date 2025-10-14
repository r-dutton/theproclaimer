[web] POST /api/lodgment/request  (DataGet.Api.Controllers.Lodgment.LodgmentController.RequestLodgementAsync)  [L31–L63]
  └─ sends_request RequestLodgmentCommand [L53]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.ApplicationService.Features.Lodgment.RequestLodgmentCommandHandler.Handle [L6–L26]
      └─ uses_service LodgmentResolver
        └─ method Resolve [L15]

