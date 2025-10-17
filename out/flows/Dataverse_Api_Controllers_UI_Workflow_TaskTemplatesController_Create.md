[web] POST /api/ui/workflow/task-templates  (Dataverse.Api.Controllers.UI.Workflow.TaskTemplatesController.Create)  [L55–L69] status=201 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ maps_to TaskTemplateDto [L68]
  └─ calls TaskTemplateRepository (methods: Add,WriteQuery) [L66]
  └─ insert TaskTemplate [L66]
    └─ reads_from TaskTemplates
  └─ write TaskTemplate [L58]
    └─ reads_from TaskTemplates
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ TaskTemplate writes=2 reads=0
    └─ mappings 1
      └─ TaskTemplateDto

