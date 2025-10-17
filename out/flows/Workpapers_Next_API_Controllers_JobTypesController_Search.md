[web] GET /api/job-types/search  (Workpapers.Next.API.Controllers.JobTypesController.Search)  [L60–L66] status=200 [auth=AuthorizationPolicies.User]
  └─ sends_request FindJobTypesQuery -> FindJobTypesQueryHandler [L63]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Workpapers.FindJobTypesQueryHandler.Handle [L32–L51]
      └─ calls JobTypeRepository.ReadQuery [L45]
  └─ impact_summary
    └─ requests 1
      └─ FindJobTypesQuery
    └─ handlers 1
      └─ FindJobTypesQueryHandler

