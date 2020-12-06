import pandas as pd
import numpy as np
from sklearn.linear_model import LinearRegression
from sklearn.metrics import mean_squared_error
import sys


class ARIMA():
    def __init__(self, p, d, q):
            self.p = p
            self.q = q
            self.d = d
    
    
    def AR(self, df):
        train_copy = df
        for i in range(1,self.p+1):
            train_copy['Shift_%d' % i ] = train_copy['data'].shift(i)

        train_copy = train_copy.dropna()

        X = train_copy.iloc[:,1:].values.reshape(-1,self.p)
        y = train_copy.iloc[:,0].values.reshape(-1,1)

        lr = LinearRegression()
        lr.fit(X,y)

        train_copy['prediction'] = X.dot(lr.coef_.T) + lr.intercept_

        return train_copy
    
    
    def MA(self,res):
        res_copy = res
        for i in range(1,self.q+1):
            res_copy['Shift_%d' % i ] = res_copy['Residuals'].shift(i)

        res_copy = res_copy.dropna()
        X = res_copy.iloc[:,1:].values.reshape(-1,self.q)
        y = res_copy.iloc[:,0].values.reshape(-1,1)

        lr = LinearRegression()
        lr.fit(X,y)

        res_copy['prediction'] = X.dot(lr.coef_.T) + lr.intercept_

        return res_copy
    
    def Predict(self, d, forecast):
        df = pd.DataFrame(data={'data': d})
        df_copy = pd.DataFrame(np.log(df.data).diff().shift(forecast))
        
        result_ar = self.AR(df_copy)
        result = pd.DataFrame()
        result['Residuals'] = result_ar.data - result_ar.prediction
        
        result_ar.prediction += (np.log(df).diff().shift(forecast).data + np.log(df).shift(1).data) 
        result_ar.data = np.exp(result_ar.data + np.log(df).shift(1).data)
        result_ar.prediction = np.exp(result_ar.prediction)
     
        return result_ar.prediction.iloc[-forecast:]



def ArimaMakePredition(array, p, d, q, f):
    model = ARIMA(p,d,q)
    prediction = model.Predict(array, f)
    return prediction.tolist()
    

f = int(sys.argv[1])
p = int(sys.argv[2])
d = int(sys.argv[3])
q = int(sys.argv[4])
data = sys.argv[5]

result =  [round(num, 2) for num in (ArimaMakePredition(list(map(float, data.split(","))), p, d, q, f))]
print(*result, sep=", ")