[web] GET /api/ui/workflow/deliverable-types  (Dataverse.Api.Controllers.UI.Workflow.DeliverableTypesController.GetAll)  [L42–L52] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to DeliverableTypeLightDto [L45]
    └─ automapper.registration DataverseMappingProfile (DeliverableType->DeliverableTypeLightDto) [L359]
  └─ calls DeliverableTypeRepository.ReadQuery [L45]
  └─ query DeliverableType [L45]
    └─ reads_from DeliverableTypes
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ DeliverableType writes=0 reads=1
    └─ mappings 1
      └─ DeliverableTypeLightDto

