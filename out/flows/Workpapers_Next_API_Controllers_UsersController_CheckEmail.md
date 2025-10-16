[web] GET /api/users/checkemail  (Workpapers.Next.API.Controllers.UsersController.CheckEmail)  [L221–L231] status=200
  └─ calls UserRepository.ReadQuery [L228]
  └─ query User [L228]
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L228]
      └─ ... (no implementation details available)

