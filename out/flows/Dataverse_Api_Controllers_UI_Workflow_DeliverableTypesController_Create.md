[web] POST /api/ui/workflow/deliverable-types  (Dataverse.Api.Controllers.UI.Workflow.DeliverableTypesController.Create)  [L64–L77] status=201 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ maps_to DeliverableTypeLightDto [L76]
  └─ calls DeliverableTypeRepository (methods: Add,WriteQuery) [L74]
  └─ insert DeliverableType [L74]
    └─ reads_from DeliverableTypes
  └─ write DeliverableType [L67]
    └─ reads_from DeliverableTypes
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ DeliverableType writes=2 reads=0
    └─ mappings 1
      └─ DeliverableTypeLightDto

