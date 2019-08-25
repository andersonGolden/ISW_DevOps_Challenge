if [ $1 == '-redis_host' ]
then
arg1="-h $2" 
else
echo Your first argument must be -redis_host
fi
if [ $3 == '-redis_port' ]
then
arg2="$arg1 -p $4"
else
echo Your third argument must be -redis_port
fi
if [ $5 == '-redisdb' ]
then
arg3="$arg2 -n $6"
else
echo Your Fifth argument must be -redisdb
fi
redis-cli $arg3
