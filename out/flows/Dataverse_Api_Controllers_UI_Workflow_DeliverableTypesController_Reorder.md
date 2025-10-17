[web] PUT /api/ui/workflow/deliverable-types/{id:Guid}/reorder  (Dataverse.Api.Controllers.UI.Workflow.DeliverableTypesController.Reorder)  [L91–L102] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls DeliverableTypeRepository.WriteQuery [L94]
  └─ write DeliverableType [L94]
    └─ reads_from DeliverableTypes
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ DeliverableType writes=1 reads=0

