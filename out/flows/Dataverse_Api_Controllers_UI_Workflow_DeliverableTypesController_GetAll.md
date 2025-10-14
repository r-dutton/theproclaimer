[web] GET /api/ui/workflow/deliverable-types  (Dataverse.Api.Controllers.UI.Workflow.DeliverableTypesController.GetAll)  [L42–L52] [auth=Authentication.UserPolicy]
  └─ maps_to DeliverableTypeLightDto [L45]
    └─ automapper.registration DataverseMappingProfile (DeliverableType->DeliverableTypeLightDto) [L358]
  └─ calls DeliverableTypeRepository.ReadQuery [L45]
  └─ queries DeliverableType [L45]
    └─ reads_from DeliverableTypes
  └─ uses_service IControlledRepository<DeliverableType>
    └─ method ReadQuery [L45]

