[web] GET /api/binders  (Workpapers.Next.API.Controllers.Workpapers.BindersController.MapDataverseIds)  [L581–L627]
  └─ calls UserRepository.ReadQuery [L606]
  └─ queries User [L606]
  └─ queries BinderStatus [L585]
    └─ reads_from BinderStatuss
  └─ uses_service IControlledRepository<BinderStatus> (AddScoped)
    └─ method ReadQuery [L585]
  └─ uses_service IControlledRepository<User>
    └─ method ReadQuery [L606]

