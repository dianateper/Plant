import numpy as np
import sys


def difference(x, d=1):
    if d == 0:
        return x
    else:
        x = np.r_[x[0], np.diff(x)]
        return difference(x, d - 1)
  

def undo_difference(x, d=1):
    if d == 1:
        return np.cumsum(x)
    else:
        x = np.cumsum(x)
        return undo_difference(x, d - 1)


def lag_view(x, order):
    y = x.copy()
    x = np.array([y[-(i + order):][:order] for i in range(y.shape[0])])
    x = np.stack(x)[::-1][order - 1: -1]
    y = y[order:]

    return x, y


def least_squares(x, y):
    return np.dot(np.linalg.inv(np.dot(x.T,x)), np.dot(x.T, y))



class LinearModel:
    def __init__(self):
        self.beta = None


    def fit(self, x, y):
        x = self._prepare_features(x)
        self.beta = least_squares(x, y)
    
        
    def _prepare_features(self, x):
        return np.hstack((np.ones((x.shape[0], 1)), x))


    def predict(self, x):
        x = self._prepare_features(x)
        return np.dot(x,self.beta)
    

    def fit_predict(self, x, y):
        self.fit(x, y)
        return self.predict(x)


class ARIMA(LinearModel):
    def __init__(self, q, d, p):
        super().__init__()
        self.p = p
        self.d = d
        self.q = q
        self.ar = None
        self.resid = None


    def fit_predict(self, x): 
        features, x = self.prepare_features(x)
        super().fit(features, x)
        features = features
        return self.predict(x, prepared=(features))


    def forecast(self, x, n):
        features, x = self.prepare_features(x)
        y = super().predict(features)
        y_len = len(y)
        y = np.r_[y, np.zeros(n)]
        
        for i in range(n):
            feat = np.r_[y[-(self.p + n) + i: -n + i], np.zeros(self.q)]
            y[x.shape[0] + i] = super().predict(feat[None, :])
        return self.return_output(y)[y_len:]


    def prepare_features(self, x):
       
        if self.d > 0:
            x = difference(x, self.d)        
        ar_features = None
        ma_features = None
    
        if self.q > 0:
            if self.ar is None:
                self.ar = ARIMA(0, 0, self.p)
                self.ar.fit_predict(x)
            eps = self.ar.resid
            eps[0] = 0
            ma_features, _ = lag_view(np.r_[np.zeros(self.q), eps], self.q)
            
        if self.p > 0:
            ar_features = lag_view(np.r_[np.zeros(self.p), x], self.p)[0]
                                
        if ar_features is not None and ma_features is not None:
            n = min(len(ar_features), len(ma_features)) 
            ar_features = ar_features[:n]
            ma_features = ma_features[:n]
            features = np.hstack((ar_features, ma_features))
            
        elif ma_features is not None: 
            n = len(ma_features)
            features = ma_features[:n]
        else:
            n = len(ar_features)
            features = ar_features[:n]
        
        return features, x[:n]
  
    

    def predict(self, x, **kwargs):
        features = kwargs.get('prepared', None)
        
        if features is None:
            features, x = self.prepare_features(x)
        
        y = super().predict(features)
        self.resid = x - y
        return self.return_output(y)
    

    def return_output(self, x):
        if self.d > 0:
            x = undo_difference(x, self.d) 
        return x
    

def ArimaMakePredition(data, p, d, q, f):
    model = ARIMA(q, d, p)
    model.fit_predict(data)
    return model.forecast(data, f)
    

f = int(sys.argv[1])
p = int(sys.argv[2])
d = int(sys.argv[3])
q = int(sys.argv[4])
data = sys.argv[5]

result =  [round(num, 2) for num in (ArimaMakePredition(list(map(float, data.split(","))), p, d, q, f).tolist())]
print(*result, sep=", ")