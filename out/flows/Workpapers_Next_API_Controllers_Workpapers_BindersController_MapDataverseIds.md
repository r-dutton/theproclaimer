[web] GET /api/binders  (Workpapers.Next.API.Controllers.Workpapers.BindersController.MapDataverseIds)  [L581–L627] status=200
  └─ calls UserRepository.ReadQuery [L606]
  └─ query User [L606]
  └─ query BinderStatus [L585]
    └─ reads_from BinderStatuss
  └─ uses_service IControlledRepository<BinderStatus> (AddScoped)
    └─ method ReadQuery [L585]
      └─ ... (no implementation details available)
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L606]
      └─ ... (no implementation details available)

