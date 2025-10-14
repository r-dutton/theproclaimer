[web] GET /api/ui/workflow/deliverable-types/{id:Guid}  (Dataverse.Api.Controllers.UI.Workflow.DeliverableTypesController.GetById)  [L54–L62] [auth=Authentication.UserPolicy]
  └─ maps_to DeliverableTypeLightDto [L57]
    └─ automapper.registration DataverseMappingProfile (DeliverableType->DeliverableTypeLightDto) [L358]
  └─ calls DeliverableTypeRepository.ReadQuery [L57]
  └─ queries DeliverableType [L57]
    └─ reads_from DeliverableTypes
  └─ uses_service IControlledRepository<DeliverableType>
    └─ method ReadQuery [L57]

