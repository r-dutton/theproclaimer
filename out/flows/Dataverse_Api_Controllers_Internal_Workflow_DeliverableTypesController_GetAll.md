[web] GET /api/internal/deliverable-types  (Dataverse.Api.Controllers.Internal.Workflow.DeliverableTypesController.GetAll)  [L53–L63] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to DeliverableTypeDto [L56]
    └─ automapper.registration DataverseMappingProfile (DeliverableType->DeliverableTypeDto) [L358]
    └─ automapper.registration ExternalApiMappingProfile (DeliverableType->DeliverableTypeDto) [L137]
  └─ calls DeliverableTypeRepository.ReadQuery [L56]
  └─ query DeliverableType [L56]
    └─ reads_from DeliverableTypes
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ DeliverableType writes=0 reads=1
    └─ mappings 1
      └─ DeliverableTypeDto

