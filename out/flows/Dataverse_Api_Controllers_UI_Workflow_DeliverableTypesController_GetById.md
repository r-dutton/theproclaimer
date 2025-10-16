[web] GET /api/ui/workflow/deliverable-types/{id:Guid}  (Dataverse.Api.Controllers.UI.Workflow.DeliverableTypesController.GetById)  [L54–L62] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to DeliverableTypeLightDto [L57]
    └─ automapper.registration DataverseMappingProfile (DeliverableType->DeliverableTypeLightDto) [L359]
  └─ calls DeliverableTypeRepository.ReadQuery [L57]
  └─ query DeliverableType [L57]
    └─ reads_from DeliverableTypes
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ DeliverableType writes=0 reads=1
    └─ mappings 1
      └─ DeliverableTypeLightDto

