[web] PUT /api/ui/workflow/job-types/{id:Guid}/reorder  (Dataverse.Api.Controllers.UI.Workflow.JobTypesController.Reorder)  [L103–L114] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls JobTypeRepository.WriteQuery [L106]
  └─ write JobType [L106]
    └─ reads_from JobTypes
  └─ uses_service IControlledRepository<JobType>
    └─ method WriteQuery [L106]
      └─ ... (no implementation details available)

