[web] GET /api/internal/job-statuses/{id}  (Dataverse.Api.Controllers.Internal.Workflow.JobStatusesController.Get)  [L51–L57] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to JobStatusDto [L54]
    └─ automapper.registration ExternalApiMappingProfile (JobStatus->JobStatusDto) [L168]
    └─ automapper.registration DataverseMappingProfile (JobStatus->JobStatusDto) [L315]
  └─ calls JobStatusRepository.ReadQuery [L54]
  └─ query JobStatus [L54]
    └─ reads_from DVS_JobStatuses
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ JobStatus writes=0 reads=1
    └─ mappings 1
      └─ JobStatusDto

