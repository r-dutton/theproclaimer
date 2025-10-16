[web] GET /api/job-types/search  (Workpapers.Next.API.Controllers.JobTypesController.Search)  [L60–L66] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request FindJobTypesQuery [L63]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.FindJobTypesQueryHandler.Handle [L32–L51]
      └─ uses_service IControlledRepository<JobType>
        └─ method ReadQuery [L45]
          └─ ... (no implementation details available)

