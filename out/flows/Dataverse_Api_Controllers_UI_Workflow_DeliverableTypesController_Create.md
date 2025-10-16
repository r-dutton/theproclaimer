[web] POST /api/ui/workflow/deliverable-types  (Dataverse.Api.Controllers.UI.Workflow.DeliverableTypesController.Create)  [L64–L77] status=201 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ maps_to DeliverableTypeLightDto [L76]
  └─ calls DeliverableTypeRepository.Add [L74]
  └─ calls DeliverableTypeRepository.WriteQuery [L67]
  └─ insert DeliverableType [L74]
    └─ reads_from DeliverableTypes
  └─ write DeliverableType [L67]
    └─ reads_from DeliverableTypes
  └─ uses_service IControlledRepository<DeliverableType>
    └─ method WriteQuery [L67]
      └─ ... (no implementation details available)
  └─ uses_service IMapper
    └─ method Map [L76]
      └─ ... (no implementation details available)

