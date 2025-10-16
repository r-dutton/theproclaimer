[web] GET /api/internal/task-templates  (Dataverse.Api.Controllers.Internal.Workflow.TaskTemplatesController.Get)  [L33–L43] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to TaskTemplateDto [L36]
    └─ automapper.registration DataverseMappingProfile (TaskTemplate->TaskTemplateDto) [L376]
    └─ automapper.registration ExternalApiMappingProfile (TaskTemplate->TaskTemplateDto) [L149]
  └─ calls TaskTemplateRepository.ReadQuery [L36]
  └─ query TaskTemplate [L36]
    └─ reads_from TaskTemplates
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ TaskTemplate writes=0 reads=1
    └─ mappings 1
      └─ TaskTemplateDto

