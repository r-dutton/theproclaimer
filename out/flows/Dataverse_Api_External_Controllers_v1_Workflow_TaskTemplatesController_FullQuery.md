[web] GET /workflow/v1/task-templates/detailed  (Dataverse.Api.External.Controllers.v1.Workflow.TaskTemplatesController.FullQuery)  [L87–L95] status=200
  └─ calls TaskTemplateRepository.ReadQuery [L91]
  └─ query TaskTemplate [L91]
    └─ reads_from TaskTemplates
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ TaskTemplate writes=0 reads=1

