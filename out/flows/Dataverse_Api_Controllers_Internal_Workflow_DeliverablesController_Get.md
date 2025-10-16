[web] GET /api/internal/deliverables/{id}  (Dataverse.Api.Controllers.Internal.Workflow.DeliverablesController.Get)  [L53–L59] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to DeliverableDto [L56]
    └─ automapper.registration ExternalApiMappingProfile (Deliverable->DeliverableDto) [L130]
    └─ automapper.registration DataverseMappingProfile (Deliverable->DeliverableDto) [L353]
  └─ calls DeliverableRepository.ReadQuery [L56]
  └─ query Deliverable [L56]
    └─ reads_from Deliverables
  └─ uses_service IControlledRepository<Deliverable>
    └─ method ReadQuery [L56]
      └─ ... (no implementation details available)

