[web] GET /api/ui/workflow/deliverable-types  (Dataverse.Api.Controllers.UI.Workflow.DeliverableTypesController.GetAll)  [L42–L52] status=200 [auth=Authentication.UserPolicy]
  └─ maps_to DeliverableTypeLightDto [L45]
    └─ automapper.registration DataverseMappingProfile (DeliverableType->DeliverableTypeLightDto) [L359]
  └─ calls DeliverableTypeRepository.ReadQuery [L45]
  └─ query DeliverableType [L45]
    └─ reads_from DeliverableTypes
  └─ uses_service IControlledRepository<DeliverableType>
    └─ method ReadQuery [L45]
      └─ ... (no implementation details available)

