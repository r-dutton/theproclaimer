[web] DELETE /api/ui/workflow/job-types/{id:Guid}  (Dataverse.Api.Controllers.UI.Workflow.JobTypesController.Delete)  [L116–L126] status=200 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ calls JobTypeRepository.WriteQuery [L119]
  └─ write JobType [L119]
    └─ reads_from JobTypes
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ JobType writes=1 reads=0

