[web] POST /api/ui/workflow/deliverable-types  (Dataverse.Api.Controllers.UI.Workflow.DeliverableTypesController.Create)  [L64–L77] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ maps_to DeliverableTypeLightDto [L76]
  └─ calls DeliverableTypeRepository.Add [L74]
  └─ calls DeliverableTypeRepository.WriteQuery [L67]
  └─ writes_to DeliverableType [L74]
    └─ reads_from DeliverableTypes
  └─ writes_to DeliverableType [L67]
    └─ reads_from DeliverableTypes
  └─ uses_service IControlledRepository<DeliverableType>
    └─ method WriteQuery [L67]
  └─ uses_service IMapper
    └─ method Map [L76]

