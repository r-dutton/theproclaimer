[web] PUT /api/ui/workflow/deliverable-types/{id:Guid}  (Dataverse.Api.Controllers.UI.Workflow.DeliverableTypesController.Update)  [L79–L89] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls DeliverableTypeRepository.WriteQuery [L82]
  └─ write DeliverableType [L82]
    └─ reads_from DeliverableTypes
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ DeliverableType writes=1 reads=0

