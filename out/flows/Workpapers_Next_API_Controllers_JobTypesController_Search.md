[web] GET /api/job-types/search  (Workpapers.Next.API.Controllers.JobTypesController.Search)  [L60–L66] [auth=AuthorizationPolicies.User]
  └─ sends_request FindJobTypesQuery [L63]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.FindJobTypesQueryHandler.Handle [L32–L51]
      └─ uses_service IControlledRepository<JobType>
        └─ method ReadQuery [L45]

