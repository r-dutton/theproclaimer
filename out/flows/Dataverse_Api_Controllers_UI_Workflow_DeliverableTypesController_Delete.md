[web] DELETE /api/ui/workflow/deliverable-types/{id:Guid}  (Dataverse.Api.Controllers.UI.Workflow.DeliverableTypesController.Delete)  [L104–L114] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls DeliverableTypeRepository.WriteQuery [L107]
  └─ write DeliverableType [L107]
    └─ reads_from DeliverableTypes
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ DeliverableType writes=1 reads=0

