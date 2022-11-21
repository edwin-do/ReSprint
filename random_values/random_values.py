# Random Number Generator
# Dumps values to csv file

import random
import csv
import time
from datetime import datetime

def Main():
	print("Hello world!\n")

	filePath = "..\\DATA.csv"

	while True:
		now = datetime.now()

		data = [
		now.strftime("%H:%M:%S"),
		random.uniform(10.0,50.0),
		random.uniform(10.0,50.0),
		random.uniform(10.0,50.0)]

		with open(filePath, 'a', encoding='UTF8', newline='') as f:
			writer = csv.writer(f)
			writer.writerow(data)
			print("Wrote new row of data\n")

		time.sleep(3)



if __name__ == "__main__":
	Main()





