[web] PUT /api/ui/workflow/job-types/{id:Guid}  (Dataverse.Api.Controllers.UI.Workflow.JobTypesController.Update)  [L91–L101] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls JobTypeRepository.WriteQuery [L94]
  └─ write JobType [L94]
    └─ reads_from JobTypes
  └─ uses_service IControlledRepository<JobType>
    └─ method WriteQuery [L94]
      └─ ... (no implementation details available)

