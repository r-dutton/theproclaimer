[web] POST /api/internal/task-templates  (Dataverse.Api.Controllers.Internal.Workflow.TaskTemplatesController.Create)  [L55–L69] status=201 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
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

