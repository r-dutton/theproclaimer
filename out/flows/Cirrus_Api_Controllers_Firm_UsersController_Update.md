[web] PUT /api/users/{id}  (Cirrus.Api.Controllers.Firm.UsersController.Update)  [L155–L163] status=200 [auth=Authentication.UserPolicy]
  └─ calls UserRepository.WriteQuery [L160]
  └─ write User [L160]
  └─ uses_service IUserService (InstancePerLifetimeScope)
    └─ method GetUserId [L158]
      └─ implementation Workpapers.Next.ApplicationService.Services.UserService.GetUserId [L20-L295]
        └─ uses_service User
          └─ method GetUserId [L67]
            └─ implementation Workpapers.Next.DomainModel.Model.Firms.User.GetUserId [L18-L368]
        └─ uses_service Guid?
          └─ method GetUserId [L64]
            └─ ... (no implementation details available)
        └─ uses_cache IMemoryCache.GetOrCreate [read] [L280]
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ User writes=1 reads=0
    └─ services 1
      └─ IUserService
    └─ caches 1
      └─ IMemoryCache

