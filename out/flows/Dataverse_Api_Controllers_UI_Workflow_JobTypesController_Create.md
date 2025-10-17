[web] POST /api/ui/workflow/job-types  (Dataverse.Api.Controllers.UI.Workflow.JobTypesController.Create)  [L76–L89] status=201 [auth=Authentication.UserPolicy,Authentication.AdminPolicy]
  └─ maps_to JobTypeDto [L88]
  └─ calls JobTypeRepository (methods: Add,WriteQuery) [L86]
  └─ insert JobType [L86]
    └─ reads_from JobTypes
  └─ write JobType [L79]
    └─ reads_from JobTypes
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ JobType writes=2 reads=0
    └─ mappings 1
      └─ JobTypeDto

