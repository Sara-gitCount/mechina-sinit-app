import { HttpInterceptorFn } from '@angular/common/http';

export const authInterceptor: HttpInterceptorFn = (req, next) => {

  const exactPublicRules = [
    { path: '/login', method: 'POST' },
    { path: '/register', method: 'POST' }
  ];

  const startsWithPublicRules = [
    { path: '/gifts/Gifts/GetGiftsByName', method: 'GET' },
    { path: '/gifts/Gifts/GetGiftsByDonorName', method: 'GET' },
    // { path: '/gifts/Gifts', method: 'GET' }

  ];

  const { pathname } = new URL(req.url);

  const isPublic =
    exactPublicRules.some(r => r.path === pathname && r.method === req.method) ||
    startsWithPublicRules.some(r => pathname.startsWith(r.path) && r.method === req.method);

  if (isPublic) {
    return next(req); // public → לא מצרפים Authorization
  }

  const token = localStorage.getItem('authToket');

  if (!token) {
    console.warn('NO TOKEN FOR:', req.method, pathname);
    return next(req);
  }

  const authReq = req.clone({
    setHeaders: {
      Authorization: `Bearer ${token}`
    }
  });

  console.log("authReq",authReq);
  
  return next(authReq);
};
