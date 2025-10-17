[web] GET /api/users/checkemail  (Workpapers.Next.API.Controllers.UsersController.CheckEmail)  [L221–L231] status=200
  └─ calls UserRepository.ReadQuery [L228]
  └─ query User [L228]
  └─ uses_service UserRepository
    └─ method ReadQuery [L228]
      └─ implementation Workpapers.Next.Data.Repository.Firms.UserRepository.ReadQuery [L9-L41]
  └─ impact_summary
    └─ entities 1 (writes=0, reads=1)
      └─ User writes=0 reads=1
    └─ services 1
      └─ UserRepository

