[web] PUT /api/ui/workflow/deliverable-types/{id:Guid}  (Dataverse.Api.Controllers.UI.Workflow.DeliverableTypesController.Update)  [L79–L89] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls DeliverableTypeRepository.WriteQuery [L82]
  └─ write DeliverableType [L82]
    └─ reads_from DeliverableTypes
  └─ uses_service IControlledRepository<DeliverableType>
    └─ method WriteQuery [L82]
      └─ ... (no implementation details available)

