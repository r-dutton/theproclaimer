[web] GET /workflow/v1/task-templates/  (Dataverse.Api.External.Controllers.v1.Workflow.TaskTemplatesController.MinimalQuery)  [L67–L75] status=200
  └─ calls TaskTemplateRepository.ReadQuery [L71]
  └─ query TaskTemplate [L71]
    └─ reads_from TaskTemplates
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ TaskTemplate writes=0 reads=1

