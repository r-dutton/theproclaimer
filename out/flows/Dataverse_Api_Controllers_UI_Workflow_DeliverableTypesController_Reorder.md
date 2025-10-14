[web] PUT /api/ui/workflow/deliverable-types/{id:Guid}/reorder  (Dataverse.Api.Controllers.UI.Workflow.DeliverableTypesController.Reorder)  [L91–L102] [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls DeliverableTypeRepository.WriteQuery [L94]
  └─ writes_to DeliverableType [L94]
    └─ reads_from DeliverableTypes
  └─ uses_service IControlledRepository<DeliverableType>
    └─ method WriteQuery [L94]

